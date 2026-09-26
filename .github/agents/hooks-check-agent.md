---
name: hooks-check-agent
description: Reads a file using only the view tool and writes a copy with `_copilot` appended before the extension using only the create tool.
tools:
  - view
  - create
custom-metadata:
  permissions:
    - "approve-read-view-all"
    - "approve-write-create-all"
model: claude-haiku-4.5
---
