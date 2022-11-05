using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public static class DbSeeder
{
  public static async Task Seed(DataContext context)
  {
    await SeedMovies(context);
    await SeedScreeningRooms(context);
    await SeedSessions(context);
  }

  private static async Task SeedMovies(DataContext context)
  {
    if (context.Movies.Any()) return;

    var movies = new List<Movie>
    {
      new Movie
      {
        Title = "Adão Negro",
        Description = "Quase 5.000 anos depois de ter sido concedido com os poderes onipotentes dos deuses egípcios - e preso com a mesma rapidez - Adão Negro é libertado de sua tumba terrena, pronto para liberar sua forma única de justiça no mundo moderno.",
        Director = "Jaume Collet-Serra",
        Genre = "Ação",
        Length = 125,
        IMDBScore = 7.1,
        ImageURL = "https://res.cloudinary.com/dxylve8nt/image/upload/c_scale,h_355,w_240/v1667405550/Cinesmac/adao-negro.jpg",
        Is3D = true,
        IsIMAX = false
      },
      new Movie
      {
        Title = "Thor: Ragnarok",
        Description = "Thor está aprisionado do outro lado do universo, sem seu martelo, e se vê em uma corrida para voltar até Asgard e impedir o Ragnarok, que está nas mãos de uma nova e poderosa ameaça, a terrível Hela.",
        Director = "Taika Waititi",
        Genre = "Ação",
        Length = 130,
        IMDBScore = 7.9,
        ImageURL = "https://res.cloudinary.com/dxylve8nt/image/upload/c_scale,h_355,w_240/v1667405549/Cinesmac/thor-ragnarok.jpg",
        Is3D = true,
        IsIMAX = false
      },
      new Movie
      {
        Title = "Mulher-Maravilha",
        Description = "Quando um piloto acidenta seu avião e encontra á Diana, uma guerrera amazonica, ela vai con ele a lutar numa guerra onde descobre seus poderes e seu verdadeiro destino.",
        Director = "Patty Jenkins",
        Genre = "Aventura",
        Length = 141,
        IMDBScore = 7.4,
        ImageURL = "https://res.cloudinary.com/dxylve8nt/image/upload/c_scale,h_355,w_240/v1667405550/Cinesmac/mulher-maravilha.jpg",
        Is3D = false,
        IsIMAX = true
      },
      new Movie
      {
        Title = "Liga da Justiça",
        Description = "Em um mundo de luto e ainda aprendendo a viver sem Superman, Bruce Wayne recruta um time de metahumanos para enfrentar um implacável senhor da guerra vindo das estrelas determinado a conquistar a Terra.",
        Director = "Zack Snyder",
        Genre = "Ação",
        Length = 242,
        IMDBScore = 8.0,
        ImageURL = "https://res.cloudinary.com/dxylve8nt/image/upload/c_scale,h_355,w_240/v1667405551/Cinesmac/liga-da-justica.jpg",
        Is3D = false,
        IsIMAX = true
      },
      new Movie
      {
        Title = "Mulher Rei",
        Description = "Um épico histórico inspirado em fatos reais ocorridos no Reino do Daomé, um dos estados mais poderosos da África nos séculos XVIII e XIX.",
        Director = "Gina Prince-Bythewood",
        Genre = "Drama",
        Length = 135,
        IMDBScore = 6.8,
        ImageURL = "https://res.cloudinary.com/dxylve8nt/image/upload/c_scale,h_355,w_240/v1667405549/Cinesmac/woman-king.jpg",
        Is3D = false,
        IsIMAX = false
      },
      new Movie
      {
        Title = "Trem-Bala",
        Description = "Cinco assassinos a bordo de um trem-bala em movimento descobrem que suas missões têm algo em comum.",
        Director = "David Leitch",
        Genre = "Suspense",
        Length = 127,
        IMDBScore = 7.3,
        ImageURL = "https://res.cloudinary.com/dxylve8nt/image/upload/c_scale,h_355,w_240/v1667405552/Cinesmac/trem-bala.jpg",
        Is3D = false,
        IsIMAX = false
      },
      new Movie
      {
        Title = "Deadpool",
        Description = "Depois de um experimento com ele, um mercenário de truques se torna imortal, mas feio, e pretende rastrear o homem que arruinou sua aparência.",
        Director = "Tim Miller",
        Genre = "Comédia",
        Length = 108,
        IMDBScore = 8.0,
        ImageURL = "https://res.cloudinary.com/dxylve8nt/image/upload/c_scale,h_355,w_240/v1667405549/Cinesmac/deadpool.jpg",
        Is3D = false,
        IsIMAX = false
      },
      new Movie
      {
        Title = "Top Gun: Maverick",
        Description = "Após mais de trinta anos de serviço como um dos melhores aviadores da Marinha, Pete Mitchell está onde pertence, ultrapassando os limites como um piloto de teste intrépido e evitando a promoção de posto que o manteria em terra.",
        Director = "Joseph Kosinski",
        Genre = "Ação",
        Length = 130,
        IMDBScore = 8.4,
        ImageURL = "https://res.cloudinary.com/dxylve8nt/image/upload/c_scale,h_355,w_240/v1667405550/Cinesmac/topgun.jpg",
        Is3D = false,
        IsIMAX = false
      },
      new Movie
      {
        Title = "O Cavaleiro das Trevas",
        Description = "Quando a ameaça conhecida como O Coringa surge de seu passado, causa estragos e caos nas pessoas de Gotham. O Cavaleiro das Trevas deve aceitar um dos maiores testes para combater a injustiça.",
        Director = "Christopher Nolan",
        Genre = "Ação",
        Length = 152,
        IMDBScore = 9.0,
        ImageURL = "https://res.cloudinary.com/dxylve8nt/image/upload/c_scale,h_355,w_240/v1667405550/Cinesmac/cavaleiro-das-trevas.jpg",
        Is3D = false,
        IsIMAX = false
      },
      new Movie
      {
        Title = "Bem-vinda a Quixeramobim",
        Description = "Uma comédia que conta a história de Aimee, uma mulher de trinta e poucos anos, herdeira de um empresário milionário envolvido em um caso de corrupção. Forçada pela primeira vez a ficar sem o dinheiro do pai para se sustentar, Aimée terá que se refugiar em sua última propriedade que resta da família: uma fazenda em ruínas em Quixeramobim, no interior do Ceará.",
        Director = "Halder Gomes",
        Genre = "Comédia",
        Length = 106,
        IMDBScore = 8.1,
        ImageURL = "https://res.cloudinary.com/dxylve8nt/image/upload/c_scale,h_355,w_240/v1667405550/Cinesmac/quixeramobim.jpg",
        Is3D = false,
        IsIMAX = false
      }
    };

    foreach (var movie in movies)
    {
      await context.Movies.AddAsync(movie);
    }
    await context.SaveChangesAsync();
  }
  private static async Task SeedScreeningRooms(DataContext context)
  {
    if (context.ScreeningRooms.Any()) return;

    var screeningRooms = new List<ScreeningRoom>{
      new ScreeningRoom{
        Name = "Sala Azul",
        Is3DRoom = true,
        IsIMAXRoom = false
      },
      new ScreeningRoom{
        Name = "Sala Verde",
        Is3DRoom = true,
        IsIMAXRoom = false
      },
      new ScreeningRoom{
        Name = "Sala Amarela",
        Is3DRoom = false,
        IsIMAXRoom = true
      },
      new ScreeningRoom{
        Name = "Sala Roxa",
        Is3DRoom = false,
        IsIMAXRoom = true
      },
      new ScreeningRoom{
        Name = "Sala Rosa",
        Is3DRoom = false,
        IsIMAXRoom = false
      },
      new ScreeningRoom{
        Name = "Sala Vermelha",
        Is3DRoom = false,
        IsIMAXRoom = false
      },
      new ScreeningRoom{
        Name = "Sala Laranja",
        Is3DRoom = false,
        IsIMAXRoom = false
      },
      new ScreeningRoom{
        Name = "Sala Preta",
        Is3DRoom = false,
        IsIMAXRoom = false
      },
      new ScreeningRoom{
        Name = "Sala Branca",
        Is3DRoom = false,
        IsIMAXRoom = false
      },
      new ScreeningRoom{
        Name = "Sala Cinza",
        Is3DRoom = false,
        IsIMAXRoom = false
      }
    };

    foreach (var screeningRoom in screeningRooms)
    {
      await context.ScreeningRooms.AddAsync(screeningRoom);
    }

    await context.SaveChangesAsync();
  }
  private static async Task SeedSessions(DataContext context)
  {
    if (context.Sessions.Any()) return;

    var movies = await context.Movies.ToListAsync();
    var screeningRooms = await context.ScreeningRooms.ToListAsync();

    for (int i = 0; i < movies.Count; i++)
    {
      var sessions = new List<Session>{
        new Session{
          StarTime = 10,
          EndTime = 12,
          BasePrice = 32,
          MovieId = movies[i].Id,
          ScreeningRoomId = screeningRooms[i].Id
        },
        new Session{
          StarTime = 13,
          EndTime = 15,
          BasePrice = 32,
          MovieId = movies[i].Id,
          ScreeningRoomId = screeningRooms[i].Id
        },
        new Session{
          StarTime = 16,
          EndTime = 18,
          BasePrice = 32,
          MovieId = movies[i].Id,
          ScreeningRoomId = screeningRooms[i].Id
        },
        new Session{
          StarTime = 19,
          EndTime = 21,
          BasePrice = 32,
          MovieId = movies[i].Id,
          ScreeningRoomId = screeningRooms[i].Id
        },
        new Session{
          StarTime = 22,
          EndTime = 0,
          BasePrice = 32,
          MovieId = movies[i].Id,
          ScreeningRoomId = screeningRooms[i].Id
        }
      };

      foreach (var session in sessions)
      {
        await context.Sessions.AddAsync(session);
      }
    }
    await context.SaveChangesAsync();
  }
}
