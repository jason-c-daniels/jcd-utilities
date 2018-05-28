#!/bin/bash
set -xe # fail on any error

usage() {
    echo "usage: $0 [--help|-h] [--build|-b] [--test|-t] [--docs|-d] [--samples|-s] [--clean|c]" >&2
}

#parse options
BUILD_SOURCE=0
BUILD_SAMPLES=0
RUN_TESTS=0
BUILD_DOCS=0
BUILD_CLEAN=0
#BUILD_NUGET=0
optspec=":chbsdt-:"
while getopts "$optspec" optchar; do
    case "${optchar}" in
        -)
            case "${OPTARG}" in
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

if [ "$BUILD_SOURCE" != 1 ] && [ "$BUILD_DOCS" != 1 ] && [ "$BUILD_SAMPLES" != 1 ] && [ "$RUN_TESTS" != 1 ]; then
    BUILD_SOURCE=1
    RUN_TESTS=1
fi

main() {

    # determine the location this script is running from.
    DIR=$(get_script_dir)
    echo $SOURCE

    pushd "$DIR/.."

	#capture the version information for the build.
    export Version=`gitversion -showvariable SemVer`
    export AssemblyVersion=`gitversion -showvariable AssemblySemVer`

    # set the default build configuration to Release, unless already set.
    if [ -z ${build_configuration+x} ]; then build_configuration="Release";  fi

    if [ "$BUILD_CLEAN" == 1 ]; then 
        # build the main library
        dotnet clean
        find docs -not -name '*.md' -not -name docs -delete
        exit 0
    fi

    if [ "$BUILD_SOURCE" == 1 ]; then 
        # build the main library
        build_folder $build_configuration "src/*/"
    fi

    if [ "$RUN_TESTS" == 1 ]; then 
        # build the tests
        build_folder $build_configuration "test/*/"
        
        # execute the tests
        execute_tests $build_configuration "test/*/"
    fi
    
    if [ "$BUILD_SAMPLES" == 1 ]; then 
        # build the sample apps.
        build_folder $build_configuration "samples/*/"
    fi

    if [ "$BUILD_DOCS" == 1 ]; then 
        # build api docs if the flag to do so was provided.
        build_docs
    fi

    # TODO: package, sign, and push to nuget

    # restore prior working dir
    popd
}

execute_tests(){
    cfg=$1
    folder=$2
    dotnet test -c $cfg $folder --no-build
}

build_folder() {
    cfg=$1
    folder=$2
    echo "building $folder"
    dotnet build -c $cfg $folder
}

build_docs() {
    # purge the old files
    find docs -not -name '*.md' -not -name docs -delete

    # generate the new
    doxygen
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

main