function(install_for_frameworks FRAMEWORKS)
    foreach(framework ${FRAMEWORKS})
        message("Framework: ${framework}")
            message("Destination: ${INSTALL_PATH_ABSOLUTE}/${framework}")
        install(TARGETS
                    TestLibrary
                COMPONENT
                    standard
                DESTINATION
                    ${INSTALL_PATH_ABSOLUTE}/${framework})
    endforeach()
endfunction()
