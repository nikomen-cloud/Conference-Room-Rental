# 🏢 System Rezerwacji Sal Konferencyjnych

## 🚀 Główne Funkcjonalności

### 👤 Panel Użytkownika
* **Rejestracja i Logowanie:** Bezpieczny system kont oparty na ASP.NET Core Identity. Automatyczne logowanie po rejestracji.
* **Katalog Sal:** Przeglądanie dostępnych sal w formie responsywnej siatki ze zdjęciami i listą udogodnień.
* **Inteligentna Rezerwacja:**
    * Dwie ścieżki rezerwacji: z poziomu katalogu oraz "Szybka rezerwacja" z menu.
    * **Wykrywanie konfliktów:** System automatycznie blokuje próbę rezerwacji zajętego terminu.
    * **Walidacja:** Blokada dat wstecznych oraz sprawdzanie poprawności przedziału czasowego (Start < Koniec).
* **Moje Rezerwacje:** Panel klienta z historią rezerwacji, statusami (Zatwierdzona/Anulowana) oraz możliwością anulowania wizyty.
* **Bilet Rezerwacji:** Widok szczegółowy rezerwacji z podsumowaniem kosztów i opcją drukowania.

### 🛡️ Panel Administratora
* **Zarządzanie Rolami (RBAC):** Dedykowany dostęp tylko dla użytkowników z rolą `Admin`.
* **Zarządzanie Salami (CRUD):**
    * Dodawanie nowych sal ze zdjęciami i opisem.
    * Edycja parametrów (cena, pojemność, wyposażenie).
    * Trwałe usuwanie sal z systemu.
* **Integracja UI:** Specjalne przyciski edycji widoczne tylko dla administratora.

## 🛠️ Technologie i Architektura

* **Backend:** C# .NET 8, ASP.NET Core MVC
* **Baza Danych:** SQLite + Entity Framework Core (Code First)
* **Frontend:** Razor Views (.cshtml), Bootstrap 5, JavaScript
* **Wzorce Projektowe:**
    * **Repository/Service Pattern:** Logika biznesowa wydzielona do serwisów (`ReservationService`, `ConferenceRoomService`).
    * **Dependency Injection:** Wstrzykiwanie zależności w kontrolerach.
    * **ViewModel:** Separacja modeli domenowych od modeli widoków.

## ⚙️ Instalacja i Uruchomienie

1.  **Klonowanie repozytorium:**
    ```bash
    git clone https://github.com/nikomen-cloud/Conference-Room-Rental
    ```
2.  **Uruchomienie aplikacji:**
    Otwórz projekt w Visual Studio lub użyj terminala:
    ```bash
    dotnet run
    ```
    *System automatycznie utworzy bazę danych i uzupełni ją przykładowymi danymi (Seed Data) przy pierwszym uruchomieniu.*

3.  **Dostęp Administratora:**
    Domyślne konto administratora tworzone przy starcie:
    * **Email:** `admin@admin.com`
    * **Hasło:** `Admin123!`

## 📊 Struktura Bazy Danych

Projekt wykorzystuje relacyjną bazę danych z kluczowymi encjami:
* **User (Identity):** Przechowuje dane logowania i role.
* **ConferenceRoom:** Przechowuje parametry sal (Cena, Pojemność, Wyposażenie).
* **Reservation:** Tabela łącząca Użytkownika z Salą w określonym czasie (Relacja Jeden-do-Wielu).
