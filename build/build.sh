#!/bin/bash
set -xe # fail on any error

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

    # build the main library
    build_folder $build_configuration "src/*/"
    
    # build the tests
    build_folder $build_configuration "test/*/"
    
    # execute the tests
    execute_tests $build_configuration "test/*/"

    # build the sample apps.
    build_folder $build_configuration "samples/*/"

    # TODO: build api docs if the flag to do so was provided.

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

build_docs(){
    echo "execute doxygen or something to build the api docs, and publish them to docs/"
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