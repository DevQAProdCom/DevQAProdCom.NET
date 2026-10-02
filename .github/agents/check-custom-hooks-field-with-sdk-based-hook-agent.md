---
name: check-custom-hooks-field-with-sdk-based-hook-agent
description: Checks SDK-based hooks. The custom-metadata.hooks field lists an SDK-based session-start hook identifier.
tools:
  - view
  - create
custom-metadata:
  permissions:
    - approve-read-view-all
    - approve-write-create-all
  hooks:
    - GitHubCopilotSdkBasedOnSessionStartHook1
model: claude-haiku-4.5
---

Health Check. Hello.
