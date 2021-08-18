How To Use
=========

This software must be compiled using dotnet 5.0

To use this application, configure the serial port on a virtual machine to point to TCP/IP.

The port for this software is 5667. You can point the emulator to 127.0.0.1:5667

This software is only intended to replace the physical hardware scanner!


Known Issues
=======

* Upload from game to emulated scanner (Broken as the protocol is a bit strange)
* Scanner Repair (not needed)
* Initial Scanner handshake (not tested, the game was handshaked against a physical scanner during testing)

Software Commands
========

* gen       - Generates 25 random scanner items that you can then download from the scanner
* erase     - Formats the memory back to 0xFF
* dump      - Shows you what the data looks like. This is mostly there for debugging