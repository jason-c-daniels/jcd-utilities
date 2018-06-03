# Build Instructions

## Prerequisites

* _dotnet core 2.0_ or higher installed and in your PATH.
* _doxygen 1.8_ or higher installed and in your PATH.
* _gitversion_ installed and in your PATH
* Either BASH and _Visual Studio Code_ (non-windows OS) or _Visual Studio 2017 Community Edition_ or later.

## Building with Visual Studio 2017 Community Edition

In the root of this repository is `Jcd.Utilities.sln`. It's a Visual Studio 2017 solution file.

* Open the solution file.
* Build the solution.
* This will build everything but the API documentation.
* To build the API documentation go to the developers command prompt and `cd` to the solution folder.
* Then type `doxygen` _`<enter>`_
* Since I will not accept pull requests including built API documentation, unstage all of these files before committing your code, or just use `git bash` and execute `build.sh -c` before executing `git add .`

## Building with `build.sh`

Execute `build.sh -a` from bash (Git bash on Windows) to build all projects, documentation, and execute unit tests.

### More build options

When typing `build.sh -h` or `build.sh --help` you get the following usage statement.

`usage: build.sh [--help|-h] [--all|-a] [--build|-b] [--test|-t] [--docs|-d] [--samples|-s] [--clean|c]`

#### Explanation

* `--help` or `-h`: displays the usage statement
* `--all` or `-a`: builds everything
* `--clean` or `-c`: removes build artifacts
* `--build` or `-b`: builds the `src` folder
* `--test` or `-t`: builds the `test` folder and executes all tests within
* `--samples` or `-s` builds the `samples` folder
* `--docs` or `-d` builds the API documentation from the `src` folder and puts the results into `docs`

#### Defaults

Providing no build options is the same as running:

`build.sh -bt`

It will build the source code and tests, and execute the tests. Documentation and samples will not be built.

#### Build Sequence

For all builds using `build.sh` the following build order is preserved, and only the selected steps are executed:

1.  Clean all build artifacts, including documentation.
2.  Build libraries in the `src` folder.
3.  Build and execute tests from the `test` folder.
4.  Build the sample apps in the `samples` folder.
5.  Build API documentation into the `docs` folder.

#### A note on the options

The single letter options can be stacked after a single dash. The long options must be written individually.

For example:

`build.sh -dc` is the same as `build.sh --docs --clean`

The order of the options in unimportant in either case.
