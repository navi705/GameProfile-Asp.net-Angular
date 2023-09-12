import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './pages/shared/modules/material.module';
import { FormsModule } from '@angular/forms';
import {
  DateAdapter,
  MAT_DATE_FORMATS,
  MAT_DATE_LOCALE,
} from '@angular/material/core';
import { MomentDateAdapter } from '@angular/material-moment-adapter';

import { AppComponent } from './app.component';
import { HeaderComponent } from './pages/shared/header/header.component';
import { GameComponent } from './pages/game-pages/game/game.component';
import { GamesComponent } from './pages/game-pages/games/games.component';
import { GameAddComponent } from './pages/game-pages/add-game/add-game.component';
import { AfterLoginSteamComponent } from './pages/profile-pages/after-login-steam/after-login-steam.component';
import { LoginComponent } from './pages/profile-pages/login/login.component';
import { ProfileComponent } from './pages/profile-pages/profile/profile.component';
import { AdminGameComponent } from './pages/admin-pages/admin-game/admin-game.component';
import { GameFiltersCreatorsComponent } from './pages/game-pages/game-filters-creators/game-filters-creators.component';
import { ProfileGamesComponentComponent } from './pages/profile-pages/profile/profile-components/profile-games-component/profile-games-component.component';
import { StatsPageComponent } from './pages/stats/stats-page/stats-page.component';
import { NotFoundComponent } from './pages/shared/NotFound/not-found/not-found.component';
import { HomePageComponent } from './pages/shared/home/home-page/home-page.component';
import { ProfileViewComponent } from './pages/profile-pages/profile/profile-components/profile-view/profile-view.component';
import { FindTeammeteSeacrhComponent } from './pages/find-temmate-pages/find-teammete-seacrh/find-teammete-seacrh.component';
import { ForumComponent } from './pages/forum-pages/forum/forum.component';

const MY_DATE_FORMAT = {
  parse: {
    dateInput: 'YYYY-MM-DD', // this is how your date will be parsed from Input
  },
  display: {
    dateInput: 'YYYY-MM-DD', // this is how your date will get displayed on the Input
    monthYearLabel: 'MMMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY'
  }
};

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
    AdminGameComponent,
    GameFiltersCreatorsComponent,
    ProfileGamesComponentComponent,
    StatsPageComponent,
    NotFoundComponent,
    HomePageComponent,
    ProfileViewComponent,
    FindTeammeteSeacrhComponent,
    ForumComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MaterialModule,
    FormsModule,
  ],
  providers: [ {
    provide: DateAdapter,
    useClass: MomentDateAdapter,
    deps: [MAT_DATE_LOCALE],
  },
  { provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMAT },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
