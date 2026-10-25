---
name: read-agent-with-2-configurations
description: Reads a file using only the view tool and returns its raw content.
tools:
  - view
model: claude-haiku-4.5
custom-metadata:
  configurations:
    - configurationIdentifier: default
      permissions:
        - approve-read-view-all
    - configurationIdentifier: filesystem-mcp-md-file-configuration
      tools:
        - filesystem/*
      permissions:
        - approve-filesystem-mcp-tools-all
      mcpServers:
        - filesystem-sdk-based-mcp-server
---
