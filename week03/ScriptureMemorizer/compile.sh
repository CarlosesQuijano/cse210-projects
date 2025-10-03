#!/bin/bash
echo "Compiling Scripture Memorizer..."
mcs *.cs -out:ScriptureMemorizer.exe
echo "Compilation complete. Run with: mono ScriptureMemorizer.exe"
