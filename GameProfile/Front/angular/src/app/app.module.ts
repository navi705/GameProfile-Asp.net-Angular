import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './modules/material.module';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { HeaderComponent } from './pages/shared/header/header.component';
import { GameComponent } from './pages/game-pages/game/game.component';
import { GamesComponent } from './pages/game-pages/games/games.component';
import { GameAddComponent } from './pages/game-pages/add-game/add-game.component';
import { AfterLoginSteamComponent } from './pages/profile-pages/after-login-steam/after-login-steam.component';
import { LoginComponent } from './pages/profile-pages/login/login.component';
import { ProfileComponent } from './pages/profile-pages/profile/profile.component';
import { AdminGameComponent } from './pages/admin-pages/admin-game/admin-game.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    GameComponent,
    GamesComponent,
    GameAddComponent,
    LoginComponent,
    AfterLoginSteamComponent,
    ProfileComponent,
    AdminGameComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MaterialModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
