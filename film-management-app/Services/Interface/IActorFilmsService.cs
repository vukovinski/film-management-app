﻿namespace film_management_app.Server
{
    public interface IActorFilmsService
    {
        Film GetById(int id);
        Film[] MyFilms(User actor);
        Film[] OtherFilms(User actor);
        Film[] InvitedFilms(User actor);
        void UpdateInformation(User actor);
    }
}
