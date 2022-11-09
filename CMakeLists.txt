project(GetMetaDataDispenser
    LANGUAGES CXX)
cmake_minimum_required(VERSION 3.0)

set(CMAKE_CXX_STANDARD 11)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)

set(CMAKE_INSTALL_PREFIX ./)

add_subdirectory(getmddisp)
add_subdirectory(external/DNCP)