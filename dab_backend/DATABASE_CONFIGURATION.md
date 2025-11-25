# Configuration des Deux Bases de Données (DAB et IAM)

## Vue d'ensemble

Le projet utilise deux bases de données PostgreSQL distinctes :
- **DAB Database** : Gère les entités liées aux DAB (Agence, Dab, Composant, Caisse, Imprimante, Lecteurcode)
- **IAM Database** : Gère les entités liées à l'authentification (Utilisateur, Rôle, Menu, Profile)

## Architecture

### DbContext
- `DABDbContext` : Gère la base DAB
- `IAMDbContext` : Gère la base IAM

### Repositories
- `GenericRepository<T>` : Pour les entités DAB (utilise DABDbContext)
- `IAMGenericRepository<T>` : Pour les entités IAM (utilise IAMDbContext)

### Unit of Work
- `IUnitOfWork` : Interface unifiée
- `UnitOfWork` : Implémentation qui gère les deux contextes

## Configuration de connexion

### appsettings.json / appsettings.Development.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=dab;Username=postgres;Password=123456",
    "IAMConnection": "Host=localhost;Database=IAM_base;Username=postgres;Password=123456"
  }
}
```

## Program.cs Configuration

Les deux contextes sont enregistrés dans Program.cs :

```csharp
builder.Services.AddDbContext<DABDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<IAMDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IAMConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
```

## Services

### Services DAB
- `IAgenceService` / `AgenceService`
- `IDabService` / `DabService`
- `IComposantService` / `ComposantService`
- `ICaisseService` / `CaisseService`
- `IImprimanteService` / `ImprimanteService`
- `ILecteurcodeService` / `LecteurcodeService`

### Services IAM
- `IUtilisateurService` / `UtilisateurService`
- `IRoleService` / `RoleService`
- `IMenuService` / `MenuService`
- `IProfileService` / `ProfileService`

## Utilisation dans les Controllers

Les services sont injectés dans les controllers qui accèdent automatiquement à la bonne base de données :

```csharp
[ApiController]
[Route("api/[controller]")]
public class UtilisateursController : ControllerBase
{
    private readonly IUtilisateurService _utilisateurService;

    public UtilisateursController(IUtilisateurService utilisateurService)
    {
        _utilisateurService = utilisateurService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UtilisateurDto>>> GetAll()
    {
        var utilisateurs = await _utilisateurService.GetAllUtilisateursAsync();
        return Ok(utilisateurs);
    }
}
```

## Sauvegarde des données

### Sauvegarder les deux bases simultanément

```csharp
await unitOfWork.SaveChangesAsync(); // Sauvegarde DAB + IAM
```

### Sauvegarder une base spécifique

```csharp
await unitOfWork.SaveChangesDABAsync();   // Sauvegarde uniquement DAB
await unitOfWork.SaveChangesIAMAsync();   // Sauvegarde uniquement IAM
```

## Endpoints API

### DAB Endpoints
- `GET/POST /api/agences`
- `GET/POST /api/dabs`
- `GET/POST /api/composants`
- `GET/POST /api/caises`
- `GET/POST /api/imprimantes`
- `GET/POST /api/lecteurcodes`

### IAM Endpoints
- `GET/POST /api/utilisateurs`
- `GET/POST /api/roles`
- `GET/POST /api/menus`
- `GET/POST /api/profiles`

## Exemple de flux complet

1. Créer un Profile dans IAM
```
POST /api/profiles
{
  "permission": "READ_DAB"
}
```

2. Créer un Rôle associé au Profile
```
POST /api/roles
{
  "nom": "Admin",
  "profileId": 1
}
```

3. Créer un Utilisateur avec ce Rôle
```
POST /api/utilisateurs
{
  "nom": "John Doe",
  "email": "john@example.com",
  "motDePasse": "password",
  "roleId": 1
}
```

4. Créer une Agence dans DAB
```
POST /api/agences
{
  "nom": "Agence Principale",
  "adresse": "123 Rue Principale"
}
```

5. Créer un DAB pour cette Agence
```
POST /api/dabs
{
  "statut": "ACTIF",
  "montantdisponible": 50000,
  "localisation": "Casablanca",
  "idagence": 1
}
```

## Notes importantes

- Chaque repository gère automatiquement sa connexion à la bonne base de données
- L'Unit of Work centralise la gestion des deux contextes
- Les transactions asynchrones sont supportées
- La validation de connexion est disponible via `DatabaseConfiguration.CheckDatabaseConnectionAsync()`
