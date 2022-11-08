set(SOURCES
  getmddisp.cpp
)

if(NOT WIN32)
  # Define for DNCP
  add_compile_definitions(
      DNCP_TYPEDEFS
      DNCP_WINHDRS
  )
  add_compile_definitions(
      COM_NO_WINDOWS_H
  )
endif()

add_library(getmddisp
  SHARED
  ${SOURCES}
)

# Only include external assets on non-Windows
if(NOT WIN32)
  target_include_directories(getmddisp PRIVATE ../external)
  target_link_libraries(getmddisp dncp_winhdrs)
endif()

target_link_libraries(getmddisp dncp)
install(TARGETS getmddisp)