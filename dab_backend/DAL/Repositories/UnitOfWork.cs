using System;
using DAL.Entities;

namespace DAL.Repositories;

/// <summary>
/// Unit of Work unified pour gérer les deux bases de données (DAB et IAM)
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly DABDbContext _dabContext;
    private readonly IAMDbContext _iamContext;
    
    // DAB Database Repositories
    private IGenericRepository<Agence> _agences;
    private IGenericRepository<Dab> _dabs;
    private IGenericRepository<Composant> _composants;
    private IGenericRepository<Caisse> _caisses;
    private IGenericRepository<Imprimante> _imprimantes;
    private IGenericRepository<Lecteurcode> _lecteurcodes;

    // IAM Database Repositories
    private IGenericRepository<Utilisateur> _utilisateurs;
    private IGenericRepository<Role> _roles;
    private IGenericRepository<Menu> _menus;
    private IGenericRepository<Profile> _profiles;

    public UnitOfWork(DABDbContext dabContext, IAMDbContext iamContext)
    {
        _dabContext = dabContext;
        _iamContext = iamContext;
    }

    // DAB Database Repositories
    public IGenericRepository<Agence> Agences
    {
        get
        {
            if (_agences == null)
                _agences = new GenericRepository<Agence>(_dabContext);
            return _agences;
        }
    }

    public IGenericRepository<Dab> Dabs
    {
        get
        {
            if (_dabs == null)
                _dabs = new GenericRepository<Dab>(_dabContext);
            return _dabs;
        }
    }

    public IGenericRepository<Composant> Composants
    {
        get
        {
            if (_composants == null)
                _composants = new GenericRepository<Composant>(_dabContext);
            return _composants;
        }
    }

    public IGenericRepository<Caisse> Caisses
    {
        get
        {
            if (_caisses == null)
                _caisses = new GenericRepository<Caisse>(_dabContext);
            return _caisses;
        }
    }

    public IGenericRepository<Imprimante> Imprimantes
    {
        get
        {
            if (_imprimantes == null)
                _imprimantes = new GenericRepository<Imprimante>(_dabContext);
            return _imprimantes;
        }
    }

    public IGenericRepository<Lecteurcode> Lecteurcodes
    {
        get
        {
            if (_lecteurcodes == null)
                _lecteurcodes = new GenericRepository<Lecteurcode>(_dabContext);
            return _lecteurcodes;
        }
    }

    // IAM Database Repositories
    public IGenericRepository<Utilisateur> Utilisateurs
    {
        get
        {
            if (_utilisateurs == null)
                _utilisateurs = new IAMGenericRepository<Utilisateur>(_iamContext);
            return _utilisateurs;
        }
    }

    public IGenericRepository<Role> Roles
    {
        get
        {
            if (_roles == null)
                _roles = new IAMGenericRepository<Role>(_iamContext);
            return _roles;
        }
    }

    public IGenericRepository<Menu> Menus
    {
        get
        {
            if (_menus == null)
                _menus = new IAMGenericRepository<Menu>(_iamContext);
            return _menus;
        }
    }

    public IGenericRepository<Profile> Profiles
    {
        get
        {
            if (_profiles == null)
                _profiles = new IAMGenericRepository<Profile>(_iamContext);
            return _profiles;
        }
    }

    public async Task SaveChangesAsync()
    {
        // Save both databases
        await Task.WhenAll(
            _dabContext.SaveChangesAsync(),
            _iamContext.SaveChangesAsync()
        );
    }

    public async Task SaveChangesDABAsync()
    {
        await _dabContext.SaveChangesAsync();
    }

    public async Task SaveChangesIAMAsync()
    {
        await _iamContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dabContext?.Dispose();
        _iamContext?.Dispose();
    }
}
