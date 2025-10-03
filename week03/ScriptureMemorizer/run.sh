#!/bin/bash
if [ -f "ScriptureMemorizer.exe" ]; then
    mono ScriptureMemorizer.exe
else
    echo "Please compile first with: bash compile.sh"
fi
