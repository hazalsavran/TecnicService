namespace Core.Entities.Concrete
{
	public class PanelUserOperationClaim : IEntity
	{
		public int Id { get; set; }
		public int PanelUserId { get; set; }
		public int OperationClaimId { get; set; }
	}
}
