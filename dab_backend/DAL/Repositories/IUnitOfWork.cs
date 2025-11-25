using System;
using DAL.Entities;

namespace DAL.Repositories;

public interface IUnitOfWork
{
    // DAB Database Repositories
    IGenericRepository<Agence> Agences { get; }
    IGenericRepository<Dab> Dabs { get; }
    IGenericRepository<Composant> Composants { get; }
    IGenericRepository<Caisse> Caisses { get; }
    IGenericRepository<Imprimante> Imprimantes { get; }
    IGenericRepository<Lecteurcode> Lecteurcodes { get; }

    // IAM Database Repositories
    IGenericRepository<Utilisateur> Utilisateurs { get; }
    IGenericRepository<Role> Roles { get; }
    IGenericRepository<Menu> Menus { get; }
    IGenericRepository<Profile> Profiles { get; }

    Task SaveChangesAsync();
    Task SaveChangesDABAsync();
    Task SaveChangesIAMAsync();
}
