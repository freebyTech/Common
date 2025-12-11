#!/bin/bash

# Start Claude - Quick launcher for Claude Code sessions
# Automatically sends initial context message

SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

cd "$SCRIPT_DIR"

echo "============================================"
echo "  Starting Claude Code Session"
echo "============================================"
echo ""
echo "📄 Sending README.md context to Claude..."
echo "📍 Working directory: $SCRIPT_DIR"
echo ""
echo "USEFUL COMMANDS"
echo "     Do the following: 1) Run 'git pull --rebase origin/develop' to get latest changes, 2) Review what files changed using 'git diff', 3)      
  Analyze how these changes affect our infrastructure setup, 4) Update README.md to reflect the current state of the infrastructure"

# Send the initial message to Claude Code
claude "Please read README.md and more importantly the CURRENT_CONTEXT.md for full context of this repository. The CURRENT_CONTEXT.md is your file to maintain your memory of this project."
