using DevQAProdCom.NET.AI.GitHubCopilot.PermissionDecisions;
using GitHub.Copilot;
using GitHub.Copilot.Rpc;

namespace DevQAProdCom.NET.AI.GitHubCopilot.Collections
{
    public class PermissionDecisionsCollection
    {
        private readonly Dictionary<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>> _decisions;

        public PermissionDecisionsCollection()
        {
            _decisions = new Dictionary<string, Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>>>();
            AddReadViewPermissionDecisions();
            AddWriteCreatePermissionDecisions();
        }

        public Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>> GetByIdentifier(string identifier)
        {
            if (_decisions.TryGetValue(identifier, out var permissionFunc))
            {
                return permissionFunc;
            }

            throw new KeyNotFoundException($"Permission decision with identifier '{identifier}' is not found in {nameof(PermissionDecisionsCollection)}.");
        }

        private void AddReadViewPermissionDecisions()
        {
            var readViewPermissionDecisions = new ReadViewPermissionDecisions();

            var approveReadViewAll = readViewPermissionDecisions.GetApproveReadViewAllPermission();
            _decisions.Add(approveReadViewAll.Key, approveReadViewAll.Value);

            var denyReadViewAll = readViewPermissionDecisions.GetDenyReadViewAllPermission();
            _decisions.Add(denyReadViewAll.Key, denyReadViewAll.Value);
        }

        private void AddWriteCreatePermissionDecisions()
        {
            var writeCreatePermissionDecisions = new WriteCreatePermissionDecisions();

            var approveWriteCreateAll = writeCreatePermissionDecisions.GetApproveWriteCreateAllPermission();
            _decisions.Add(approveWriteCreateAll.Key, approveWriteCreateAll.Value);

            var denyWriteCreateAll = writeCreatePermissionDecisions.GetDenyWriteCreateAllPermission();
            _decisions.Add(denyWriteCreateAll.Key, denyWriteCreateAll.Value);
        }
    }
}
