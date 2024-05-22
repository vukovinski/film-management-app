namespace film_management_app.Server
{
    public interface IFeeNegotiationService
    {
        void Accept(FeeNegotiation feeNegotiation);
        void Decline(FeeNegotiation feeNegotiation);
        FeeNegotiation Create(FilmStar actor, decimal newFee);
    }
}
