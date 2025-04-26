# CanineConnect

A friendly .NET MAUI mobile app to help dog lovers track and discover pups. Users log in, browse their personal “Your Pups” list, and add new furry friends — all with a clean, Figma-inspired UI.

---

## 📱 Screenshots

| Login Modal | Create Account Modal |
|:-----------:|:--------------------:|
| ![LognModal](https://github.com/user-attachments/assets/6710a5b1-d784-4c94-bead-35f73238639c) | ![CreateAnAccountModal](https://github.com/user-attachments/assets/20f92362-3372-45fa-8adc-bf2103f0ee7a) |

| Email Verification | Home (“Your Pups”) |
|:------------------:|:------------------:|
| ![EmailAuthenticationEmail](https://github.com/user-attachments/assets/767c35b8-12b8-4063-aff6-45cea59a83f9) | ![Home_alsoYourPets](https://github.com/user-attachments/assets/8ca3fe1b-ccd6-4b92-a6ca-58040aa8959c) |

| Navigation Bar | Home Screen |
|:-------------:|:-----------:|
| ![NavBar](https://github.com/user-attachments/assets/e53e4318-d510-40ec-ab38-d7463d1bfe29) | ![HomeScreen](https://github.com/user-attachments/assets/8f09ff82-f862-4cc3-8453-3eab74f14b1e) |

---

## 🔥 Features

- **Email/password authentication** with verification link  
- **Persistent login** using Bearer tokens  
- **Your Pups** list: view, add, and track details (name, age, gender, size, breed, image)  
- **Figma-style design** with bright theme, rounded cards, and subtle shadows  
- **Tabbed navigation**: Home, Breed Match, Messages, Pups Near You  

---

## 🚀 Project Flow

1. **Login Page**  
   - User taps **Log In** or **Create Account**  
   - If new, enters email & password, receives verification email  
   - Once verified, signs in  

2. **Home (“Your Pups”)**  
   - Displays a scrollable list of user’s saved pups  
   - Each pup card shows an image and key details  
   - “Add Another Pup” button (stubbed for now)  

3. **Add Pets**  
   - (Future) Modal/page to capture pup information and image  
   - Upon save, new card appears in **Your Pups**  

4. **Other Tabs**  
   - **Breed Match**: get recommended breeds  
   - **Messages**: chat or notifications  
   - **Pups Near You**: find nearby adoptable pups  

---

## 🛠️ Tech Stack

- **.NET MAUI** for cross-platform UI  
- **XAML + MVVM**-style binding (or code-behind)  
- **PocketBase API** (JSON) for user & pet data  
- **System.Text.Json** to parse local or remote JSON  
- **FileSystem.OpenAppPackageFileAsync** for bundled assets  

---

## 💡 Getting Started

1. **Clone this repo**  
   ```bash
   git clone https://github.com/your-org/CanineConnect.git
