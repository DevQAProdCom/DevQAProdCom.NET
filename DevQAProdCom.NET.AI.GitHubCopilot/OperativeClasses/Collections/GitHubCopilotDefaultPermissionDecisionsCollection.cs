using DevQAProdCom.NET.AI.GitHubCopilot.Interfaces;
using DevQAProdCom.NET.AI.GitHubCopilot.Models;
using DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.PermissionDecisions;
using DevQAProdCom.NET.AI.Shared.OperativeClasses;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.AI.GitHubCopilot.OperativeClasses.Collections
{
    public class GitHubCopilotDefaultPermissionDecisionsCollection : IdentifierBasedEntitiesCollection<IGitHubCopilotPermissionDecision>
    {
        public GitHubCopilotDefaultPermissionDecisionsCollection(ILogger logger, string? collectionIdentifier = null)
            : base(logger, collectionIdentifier)
        {
            AddReadViewPermissionDecisions();
            AddWriteCreatePermissionDecisions();
            AddWriteEditPermissionDecisions();
            AddPlaywrightMcpPermissionDecisions();
            AddFilesystemMcpPermissionDecisions();
        }

        private void AddReadViewPermissionDecisions()
        {
            var readViewPermissionDecisions = new ReadViewPermissionDecisions();

            var approveReadViewAll = readViewPermissionDecisions.GetApproveReadViewAllPermission();
            Add(new GitHubCopilotPermissionDecisionModel
            {
                Identifier = approveReadViewAll.Key,
                PermissionDecisionFunc = approveReadViewAll.Value
            });

            var denyReadViewAll = readViewPermissionDecisions.GetDenyReadViewAllPermission();
            Add(new GitHubCopilotPermissionDecisionModel
            {
                Identifier = denyReadViewAll.Key,
                PermissionDecisionFunc = denyReadViewAll.Value
            });
        }

        private void AddWriteCreatePermissionDecisions()
        {
            var writeCreatePermissionDecisions = new WriteCreatePermissionDecisions();

            var approveWriteCreateAll = writeCreatePermissionDecisions.GetApproveWriteCreateAllPermission();
            Add(new GitHubCopilotPermissionDecisionModel
            {
                Identifier = approveWriteCreateAll.Key,
                PermissionDecisionFunc = approveWriteCreateAll.Value
            });

            var denyWriteCreateAll = writeCreatePermissionDecisions.GetDenyWriteCreateAllPermission();
            Add(new GitHubCopilotPermissionDecisionModel
            {
                Identifier = denyWriteCreateAll.Key,
                PermissionDecisionFunc = denyWriteCreateAll.Value
            });
        }

        private void AddWriteEditPermissionDecisions()
        {
            var writeEditPermissionDecisions = new WriteEditPermissionDecisions();

            var approveWriteEditAll = writeEditPermissionDecisions.GetApproveWriteEditAllPermission();
            Add(new GitHubCopilotPermissionDecisionModel
            {
                Identifier = approveWriteEditAll.Key,
                PermissionDecisionFunc = approveWriteEditAll.Value
            });

            var denyWriteEditAll = writeEditPermissionDecisions.GetDenyWriteEditAllPermission();
            Add(new GitHubCopilotPermissionDecisionModel
            {
                Identifier = denyWriteEditAll.Key,
                PermissionDecisionFunc = denyWriteEditAll.Value
            });
        }

        private void AddPlaywrightMcpPermissionDecisions()
        {
            var playwrightMcpPermissionDecisions = new PlaywrightMcpPermissionDecisions();

            var approvePlaywrightMcpAll = playwrightMcpPermissionDecisions.GetApprovePlaywrightMcpAllPermission();
            Add(new GitHubCopilotPermissionDecisionModel
            {
                Identifier = approvePlaywrightMcpAll.Key,
                PermissionDecisionFunc = approvePlaywrightMcpAll.Value
            });
        }

        private void AddFilesystemMcpPermissionDecisions()
        {
            var filesystemMcpPermissionDecisions = new FilesystemMcpPermissionDecisions();

            var approveFilesystemMcpAll = filesystemMcpPermissionDecisions.GetApproveFilesystemMcpAllPermission();
            Add(new GitHubCopilotPermissionDecisionModel
            {
                Identifier = approveFilesystemMcpAll.Key,
                PermissionDecisionFunc = approveFilesystemMcpAll.Value
            });
        }
    }
}
