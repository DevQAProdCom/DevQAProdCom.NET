---
name: hooks-check-agent
description: Checks hooks. The custom-metadata.hooks field lists hook identifiers such as session-start-hook-1, which runs a PowerShell script at session start.
tools:
  - view
  - create
custom-metadata:
  permissions:
    - approve-read-view-all
    - approve-write-create-all
  hooks:
    - on-session-start-hook-1
    - OnSessionStartHook1
model: claude-haiku-4.5
---

Health Check. Hello.