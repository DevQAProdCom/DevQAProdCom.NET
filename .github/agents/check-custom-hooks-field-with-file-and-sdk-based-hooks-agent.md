---
name: check-custom-hooks-field-with-file-and-sdk-based-hooks-agent
description: Checks both file-based and SDK-based hooks. The custom-metadata.hooks field lists identifiers for a file-based PowerShell hook and an SDK-based session-start hook.
tools:
  - view
  - create
custom-metadata:
  permissions:
    - approve-read-view-all
    - approve-write-create-all
  hooks:
    - on-session-start-hook-1
    - GitHubCopilotSdkBasedOnSessionStartHook1
model: claude-haiku-4.5
---

Health Check. Hello.
