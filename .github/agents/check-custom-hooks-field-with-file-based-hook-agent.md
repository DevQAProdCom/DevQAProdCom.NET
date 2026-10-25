---
name: check-custom-hooks-field-with-file-based-hook-agent
description: Checks file-based hooks. The custom-metadata.hooks field lists a file-based hook identifier that runs a PowerShell script at session start.
tools:
  - view
  - create
model: claude-haiku-4.5
custom-metadata:
  configurations:
    - permissions:
        - approve-read-view-all
        - approve-write-create-all
      hooks:
        - on-session-start-hook-1
---

Health Check. Hello.
