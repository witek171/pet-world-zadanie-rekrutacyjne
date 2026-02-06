# 🐾 PetWorld - AI Chat sklepu internetowego

## 📖 Opis projektu

**PetWorld** to aplikacja webowa sklepu internetowego oferującego produkty dla zwierząt domowych. Klienci mogą zadawać pytania o produkty poprzez chat, a system AI pomaga im znaleźć odpowiednie produkty i udziela porad dotyczących opieki nad zwierzętami.

### Główne cechy:
- 💬 **Inteligentny chat** - asystent AI odpowiada na pytania klientów
- 🔄 **Writer-Critic workflow** - system iteracyjnego ulepszania odpowiedzi
- 📊 **Historia rozmów** - pełna historia wszystkich konwersacji
- 🏗️ **Clean Architecture** - projekt zgodny z zasadami Onion/Clean Architecture
- 🐳 **Docker** - łatwe uruchomienie jednym poleceniem

## 🚀 Uruchomienie

1. Edytuj plik `src/PetWorld.Web/appsettings.json`

```aiignore
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

## 🏗️ Architektura
- **Domain** - Encje (Product, ChatConversation)
- **Application** - Interfejsy, Serwisy, DTOs
- **Infrastructure** - Repozytoria MySQL, Agenci AI
- **Web** - Blazor Server UI

## 🔄 Writer-Critic
System AI z maksymalnie 3 iteracjami:
1. Writer generuje odpowiedz
2. Critic ocenia (approved: true/false)
3. Jesli false - Writer poprawia na podstawie feedbacku
   
## 🛒 Katalog produktów
Produkty są automatycznie ładowane do bazy MySQL podczas pierwszego uruchomienia

## 📁 Struktura projektu

