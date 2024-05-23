namespace film_management_app.Server
{
    public class FeeNegotiationService : IFeeNegotiationService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IStarRepository _starRepository;

        public FeeNegotiationService(IFilmRepository filmRepository, IStarRepository starRepository)
        {
            _filmRepository = filmRepository;
            _starRepository = starRepository;
        }

        public FeeNegotiation Create(FilmStar actor, decimal newFee)
        {
            var film = _filmRepository.GetById(actor.FilmId);
            var feeNegotiation = new FeeNegotiation
            {
                FilmId = film.Id,
                NewFee = newFee,
                UserId = actor.UserId,
                OldFee = film.Actors.First(a => a.UserId == actor.UserId).Fee
            };
            film.FeeNegotiations.Add(feeNegotiation);
            _filmRepository.Update(film);
            return feeNegotiation;
        }

        public void Accept(FeeNegotiation feeNegotiation)
        {
            var starring = _starRepository.GetFilmStarByFilmIdAndUserId(feeNegotiation.FilmId, feeNegotiation.UserId);
            starring.Fee = feeNegotiation.NewFee;
            RemoveNegotiation(feeNegotiation);
            _starRepository.Update(starring);
        }

        public void Decline(FeeNegotiation feeNegotiation)
        {
            var starring = _starRepository.GetFilmStarByFilmIdAndUserId(feeNegotiation.FilmId, feeNegotiation.UserId);
            RemoveNegotiation(feeNegotiation);
            _starRepository.Delete(starring);
        }

        private void RemoveNegotiation(FeeNegotiation feeNegotiation)
        {
            var actorId = feeNegotiation.UserId;
            var film = _filmRepository.GetById(feeNegotiation.FilmId);
            film.FeeNegotiations = film.FeeNegotiations.Where(fn => fn.UserId != actorId).ToList();

            _filmRepository.Update(film);
        }
    }
}
