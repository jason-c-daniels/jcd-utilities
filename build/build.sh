#!/bin/bash
set -xe # fail on any error

#parse options
BUILD_SOURCE=0
BUILD_SAMPLES=0
RUN_TESTS=0
BUILD_DOCS=0
BUILD_CLEAN=0

optspec=":cahbsdt-:"
while getopts "$optspec" optchar; do
    case "${optchar}" in
        -)
            case "${OPTARG}" in
                all)
                    BUILD_CLEAN=1
                    BUILD_SOURCE=1
                    BUILD_SAMPLES=1
                    RUN_TESTS=1
                    BUILD_DOCS=1
                    ;;
                clean)
                    BUILD_CLEAN=1
                    ;;
                build)
                    BUILD_SOURCE=1
                    ;;
                samples)
                    BUILD_SAMPLES=1
                    ;;
                test)
                    RUN_TESTS=1
                    ;;
                docs)
                    BUILD_DOCS=1
                    ;;
                help)
                    usage
                    exit 0;
                    ;;
                *)
                    if [ "$OPTERR" != 1 ] || [ "${optspec:0:1}" = ":" ]; then
                        echo "unrecognized option/arg: '--${OPTARG}'" >&2
                        usage
                        exit 1
                    fi
                    ;;
            esac;;
        h)
            usage
            exit 0
            ;;
        a)
            BUILD_CLEAN=1
            BUILD_SOURCE=1
            BUILD_SAMPLES=1
            RUN_TESTS=1
            BUILD_DOCS=1
            ;;
        c)
            BUILD_CLEAN=1
            ;;
        b)
            BUILD_SOURCE=1
            ;;
        d)
            BUILD_DOCS=1
            ;;
        s)
            BUILD_SAMPLES=1
            ;;
        t)
            RUN_TESTS=1
            ;;
        *)
            if [ "$OPTERR" != 1 ] || [ "${optspec:0:1}" = ":" ]; then
                echo "unrecognized option/arg: '-${OPTARG}'" >&2
                usage
                exit 1
            fi
            ;;
    esac
done

if [ "$BUILD_CLEAN" != 1 ] && [ "$BUILD_SOURCE" != 1 ] && [ "$BUILD_DOCS" != 1 ] && [ "$BUILD_SAMPLES" != 1 ] && [ "$RUN_TESTS" != 1 ]; then
    BUILD_SOURCE=1
    RUN_TESTS=1
fi

export BUILD_SOURCE
export BUILD_SAMPLES
export RUN_TESTS
export BUILD_DOCS
export BUILD_CLEAN   

main() {
    
    echo $BUILD_CLEAN "clean"
    echo $BUILD_SOURCE "source"
    #TODO: nix GitVersion, it's only good on Windows. Find some other way to do it.

    # determine the location this script is running from.
    DIR=$(get_script_dir)

    pushd "$DIR/.."

	# capture the version information for the build.    
    if [ -z ${Configuration+x} ]; then Configuration="Release"; fi
    if [ -z ${VersionPrefix+x} ]; then VersionPrefix=$(get_prefix); fi
    if [ -z ${VersionSuffix+x} ]; then VersionSuffix=$(get_suffix); fi
    if [ -z ${Version+x} ]; then 
        # do nothing this is exactly what we want.
        echo "Version was not set"
    else
        unset -v Version
    fi
    export SemanticVersion="$VersionPrefix-$VersionSuffix"
    
    # set the default build configuration to Release, unless already set.
    export Configuration
    export VersionPrefix
    export VersionSuffix

    if [ "$BUILD_CLEAN" == 1 ]; then 
        # cleanup build artifacts
        (unset -v Version; dotnet clean)
        find . -type d -name obj -prune -exec rm -rf {} \;
        find . -type d -name bin -prune -exec rm -rf {} \;
        clean_docs
    fi
    
    if [ "$BUILD_SOURCE" == 1 ]; then 
        # build the main library
        build_folder "./src"
        pkg_folder="$(pwd)/packages";
        mkdir -p "$pkg_folder"
        pack_folder "./src" "$pkg_folder"
    fi

    if [ "$RUN_TESTS" == 1 ]; then 
        # build the tests
        build_folder "./test"
        
        # execute the tests
        execute_tests "./test"
    fi
    
    if [ "$BUILD_SAMPLES" == 1 ]; then 
        # build the sample apps.
        build_folder "./samples"
    fi

    if [ "$BUILD_DOCS" == 1 ]; then 
        # build api docs if the flag to do so was provided.
        build_docs
    fi

    # TODO: package, sign, and push to nuget
    echo $?
    # restore prior working dir
    popd
}

usage() {
    echo "usage: $0 [--help|-h] [--all|-a] [--build|-b] [--test|-t] [--docs|-d] [--samples|-s] [--clean|c]" >&2
}

execute_tests() {
    folder=$1
    #find "$folder" -maxdepth 2 -type f -name "*.csproj" -print0 | xargs -0 -n1 dotnet test -c "$cfg" 
    find "$folder" -maxdepth 2 -type f -name "*.csproj" -print0 | xargs -0 -n1 dotnet test
}

build_folder() {
    folder=$1
    echo "building $folder"
    find "$folder" -maxdepth 2 -type f -name "*.csproj" -print0 | xargs -0 -n1 dotnet build
    #dotnet build -c "$cfg" "$folder"
}

pack_folder() {
    folder=$1
    pkg_folder=$2
    echo "building $folder"
    find "$folder" -maxdepth 2 -type f -name "*.csproj" -print0 | xargs -0 -n1 dotnet pack -o "$pkg_folder" --no-build
}

clean_packages() {
    rm -rf "./packages"
}

clean_docs() {
    find docs -not -name '*.md' -not -name docs -delete
}

build_docs() {
    clean_docs
    local suffix="$(get_suffix)"
    ProjectNumber="$(get_prefix)"
    if [[ "$suffix" != "" ]]; then ProjectNumber="$ProjectNumber-$suffix";  fi
    export ProjectNumber
    # generate the new API docs
    ( cat Doxyfile ; echo "PROJECT_NUMBER=$ProjectNumber" ) | doxygen -
}

get_script_dir () {
     SOURCE="${BASH_SOURCE[0]}"
     # While $SOURCE is a symlink, resolve it
     while [ -h "$SOURCE" ]; do
          DIR="$( cd -P "$( dirname "$SOURCE" )" && pwd )"
          SOURCE="$( readlink "$SOURCE" )"
          # If $SOURCE was a relative symlink (so no "/" as prefix, need to resolve it relative to the symlink base directory
          [[ $SOURCE != /* ]] && SOURCE="$DIR/$SOURCE"
     done
     DIR="$( cd -P "$( dirname "$SOURCE" )" && pwd )"
     echo "$DIR"
}

git_rev() {
    echo $(git rev-list $(git rev-list --tags --no-walk --max-count=1)..HEAD --count)
}

get_prefix() {
    local prefix="$(git rev-parse --symbolic --tags | sort -i | tail -1 | sed -e 's/\(.*\)\([-].*\)/\1/')"

    if [[ "$APPVEYOR" == "true" ]]; then
        prefix=$APPVEYOR_BUILD_VERSION
    fi

    if [[ "$prefix" == "" ]]; then prefix="0.0.$(git_rev)"; fi
    echo $prefix
}

get_suffix() {
    # local system defaults. These are overridden by appveyor
    local rev=$(git_rev)
    local suffix="$(git rev-parse --symbolic --tags | sort -i | tail -1 | sed -e 's/\(.*\)\([-]\)\(.*\)/\3/').$rev"
    local branch_type=$(git rev-parse --abbrev-ref HEAD | sed -e 's/\(develop\|master\|feature\)\(.*\)$/\1/')
    if [[ "$APPVEYOR" == "true" ]]; then
        rev=$APPVEYOR_PULL_REQUEST_NUMBER
    fi

    if [[ "$branch_type" == "master" ]]; then suffix=""; fi
    if [[ "$branch_type" == "develop" ]]; then suffix="rc.$rev"; fi
    if [[ "$branch_type" == "feature" ]]; then suffix="pre.$rev"; fi


    echo $suffix
}

# now do the work!
main
