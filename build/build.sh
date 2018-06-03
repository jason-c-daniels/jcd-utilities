#!/bin/bash
set -xe # fail on any error

usage() {
    echo "usage: $0 [--help|-h] [--all|-a] [--build|-b] [--test|-t] [--docs|-d] [--samples|-s] [--clean|c]" >&2
}

#parse options
BUILD_SOURCE=0
BUILD_SAMPLES=0
RUN_TESTS=0
BUILD_DOCS=0
BUILD_CLEAN=0
#BUILD_NUGET=0
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

main() {
    set -xe 
    # determine the location this script is running from.
    DIR=$(get_script_dir)
    echo $SOURCE

    pushd "$DIR/.."

	#capture the version information for the build.
    export Version=$(gitversion -showvariable SemVer)
    export AssemblyVersion=$(gitversion -showvariable AssemblySemVer)

    # set the default build configuration to Release, unless already set.
    if [ -z ${build_configuration+x} ]; then build_configuration="Release";  fi

    if [ "$BUILD_CLEAN" == 1 ]; then 
        # cleanup build artifacts
        dotnet clean
        find . -type d -name obj -prune -exec rm -rf {} \;
        find . -type d -name bin -prune -exec rm -rf {} \;
        clean_docs
    fi

    if [ "$BUILD_SOURCE" == 1 ]; then 
        # build the main library
        build_folder $build_configuration "./src"
        pkg_folder="$(pwd)/packages";
        mkdir -p "$pkg_folder"
        pack_folder $build_configuration "./src" "$pkg_folder"
    fi

    if [ "$RUN_TESTS" == 1 ]; then 
        # build the tests
        build_folder $build_configuration "./test"
        
        # execute the tests
        execute_tests $build_configuration "./test"
    fi
    
    if [ "$BUILD_SAMPLES" == 1 ]; then 
        # build the sample apps.
        build_folder $build_configuration "./samples"
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

execute_tests(){
    set -xe 
    cfg=$1
    folder=$2
    find "$folder" -maxdepth 2 -type f -name "*.csproj" -print0 | xargs -0 -n1 dotnet test -c "$cfg" 
}

build_folder() {
    set -xe 
    cfg=$1
    folder=$2
    echo "building $folder"
    find "$folder" -maxdepth 2 -type f -name "*.csproj" -print0 | xargs -0 -n1 dotnet build -c "$cfg" 
}

pack_folder() {
    set -xe 
    cfg=$1
    folder=$2
    pkg_folder=$3
    echo "building $folder"
    find "$folder" -maxdepth 2 -type f -name "*.csproj" -print0 | xargs -0 -n1 dotnet pack -o "$pkg_folder" --no-build -c "$cfg" 
}

clean_packages() {
    set -xe 
    #find docs -not -name '*.md' -not -name docs -delete
    rm -rf "./packages"
}

clean_docs() {
    set -xe 
    find docs -not -name '*.md' -not -name docs -delete
}

build_docs() {
    set -xe 
    clean_docs

    export ProjectNumber=$(gitversion -showvariable SemVer)
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

# now do the work!
main
echo $?
exit $?