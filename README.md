# 🐾 PetWorld - AI Chat sklepu internetowego

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![Blazor](https://img.shields.io/badge/Blazor-512BD4?style=for-the-badge&logo=blazor&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework_Core-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)
![OpenAI](https://img.shields.io/badge/OpenAI-412991?style=for-the-badge&logo=openai&logoColor=white)

## 📖 Opis projektu

**PetWorld** to aplikacja webowa sklepu internetowego oferującego produkty dla zwierząt domowych. Klienci mogą zadawać
pytania o produkty poprzez chat, a system AI pomaga im znaleźć odpowiednie produkty i udziela porad dotyczących opieki
nad zwierzętami

### Główne cechy:

- 💬 **Inteligentny chat** - asystent AI odpowiada na pytania klientów
- 🔄 **Writer-Critic workflow** - system iteracyjnego ulepszania odpowiedzi
- 📊 **Historia rozmów** - pełna historia wszystkich konwersacji
- 🏗️ **Clean Architecture** - projekt zgodny z zasadami Onion/Clean Architecture
- 🐳 **Docker** - łatwe uruchomienie jednym poleceniem

## 🚀 Uruchomienie

1. Edytuj plik `src/PetWorld.Web/appsettings.json`

```
"OpenAI": {
  "ApiKey": "sk-twoj-klucz-api",
  "ModelId": "gpt-4o-mini"
}
```

2. Uruchom: `docker compose up`
3. Otworz: http://localhost:5000

### Co się dzieje podczas uruchomienia?

- 🐳 Docker buduje obraz aplikacji .NET 8
- 🗄️ Uruchamia się kontener MySQL 8.0
- 📦 Skrypt mysql-init/init.sql tworzy tabele i wstawia produkty
- 🌐 Aplikacja Blazor Server startuje na porcie 5000
- ✅ Aplikacja czeka na gotowość bazy danych (healthcheck)

## 🔄 Writer-Critic

System AI z maksymalnie 3 iteracjami:

1. Writer generuje odpowiedz
2. Critic ocenia (approved: true/false)
3. Jesli false - Writer poprawia na podstawie feedbacku

## 🛒 Katalog produktów

Produkty są automatycznie ładowane do bazy MySQL podczas pierwszego uruchomienia

| Nazwa produktu                 | Kategoria           | Cena   | Opis                                            |
|--------------------------------|---------------------|--------|-------------------------------------------------|
| Royal Canin Adult Dog 15kg     | Karma dla psów      | 289 zł | Premium karma dla dorosłych psów średnich ras   |
| Whiskas Adult Kurczak 7kg      | Karma dla kotów     | 129 zł | Sucha karma dla dorosłych kotów z kurczakiem    |
| Tetra AquaSafe 500ml           | Akwarystyka         | 45 zł  | Uzdatniacz wody do akwarium, neutralizuje chlor |
| Trixie Drapak XL 150cm         | Akcesoria dla kotów | 399 zł | Wysoki drapak z platformami i domkiem           |
| Kong Classic Large             | Zabawki dla psów    | 69 zł  | Wytrzymała zabawka do napełniania smakołykami   |
| Ferplast Klatka dla chomika    | Gryzonie            | 189 zł | Klatka 60x40cm z wyposażeniem                   |
| Flexi Smycz automatyczna 8m    | Akcesoria dla psów  | 119 zł | Smycz zwijana dla psów do 50kg                  |
| Brit Premium Kitten 8kg        | Karma dla kotów     | 159 zł | Karma dla kociąt do 12 miesiąca życia           |
| JBL ProFlora CO2 Set           | Akwarystyka         | 549 zł | Kompletny zestaw CO2 dla roślin akwariowych     |
| Vitapol Siano dla królików 1kg | Gryzonie            | 25 zł  | Naturalne siano łąkowe, podstawa diety          |

## 📁 Struktura projektu

```
PetWorld/
├── 📄 PetWorld.sln                    # Solution file
├── 📄 Directory.Build.props           # Wspólne ustawienia MSBuild dla wszystkich projektów
├── 📄 .editorconfig                   # Konfiguracja stylu kodu i formatowania
├── 📄 docker-compose.yml              # Konfiguracja Docker Compose
├── 📄 Dockerfile                      # Dockerfile dla aplikacji
├── 📄 README.md                       # Dokumentacja projektu
├── 📄 .gitignore                      # Git ignore
│
├── 📁 mysql-init/
│   └── 📄 init.sql                    # Skrypt inicjalizacji bazy danych
│
└── 📁 src/
    │
    ├── 📁 PetWorld.Domain/            # 🎯 WARSTWA DOMENOWA
    │   ├── 📄 PetWorld.Domain.csproj
    │   └── 📁 Entities/
    │       ├── 📄 Product.cs          # Encja produktu
    │       ├── 📄 ChatConversation.cs # Encja konwersacji
    │       ├── 📄 AgentResponse.cs    # Odpowiedź agenta AI
    │       └── 📄 CriticFeedback.cs   # Feedback od Critic
    │
    ├── 📁 PetWorld.Application/       # 📋 WARSTWA APLIKACYJNA
    │   ├── 📄 PetWorld.Application.csproj
    │   ├── 📁 Interfaces/
    │   │   ├── 📄 IChatRepository.cs      # Interfejs repo konwersacji
    │   │   ├── 📄 IProductRepository.cs   # Interfejs repo produktów
    │   │   ├── 📄 IAgentService.cs        # Interfejs serwisu AI
    │   │   └── 📄 IProductService.cs      # Interfejs serwisu produktów
    │   ├── 📁 Services/
    │   │   └── 📄 ChatService.cs          # Serwis czatu
    │   └── 📁 DTOs/
    │       ├── 📄 ChatRequest.cs          # DTO żądania
    │       └── 📄 ChatResponse.cs         # DTO odpowiedzi
    │
    ├── 📁 PetWorld.Infrastructure/    # 🔧 WARSTWA INFRASTRUKTURY
    │   ├── 📄 PetWorld.Infrastructure.csproj
    │   ├── 📁 Data/
    │   │   └── 📄 PetWorldDbContext.cs    # Kontekst EF Core
    │   ├── 📁 Repositories/
    │   │   ├── 📄 ChatRepository.cs       # Implementacja repo konwersacji
    │   │   └── 📄 ProductRepository.cs    # Implementacja repo produktów
    │   ├── 📁 Services/
    │   │   └── 📄 ProductService.cs       # Implementacja serwisu produktów
    │   └── 📁 Agents/
    │       └── 📄 WriterCriticAgentService.cs  # System Writer-Critic
    │
    └── 📁 PetWorld.Web/               # 🌐 WARSTWA PREZENTACJI
        ├── 📄 PetWorld.Web.csproj
        ├── 📄 Program.cs                  # Entry point + DI
        ├── 📄 appsettings.json            # Konfiguracja
        ├── 📄 appsettings.Development.json
        ├── 📄 App.razor                   # Root component
        ├── 📄 _Imports.razor              # Global usings
        ├── 📁 Pages/
        │   ├── 📄 _Host.cshtml            # Host page
        │   ├── 📄 Index.razor             # Strona czatu
        │   └── 📄 History.razor           # Strona historii
        ├── 📁 Shared/
        │   └── 📄 MainLayout.razor        # Główny layout
        └── 📁 wwwroot/
            └── 📁 css/
                └── 📄 site.css            # Style CSS
```

## 📸 Prezentacja interfejsu

<img width="1917" height="940" alt="Image" src="https://github.com/user-attachments/assets/bdf26175-94f7-4ed0-9c69-635fd123c33e" />

<img width="1917" height="940" alt="Image" src="https://github.com/user-attachments/assets/77cfcc07-7bdb-4698-a24c-f58fb419c115" />

