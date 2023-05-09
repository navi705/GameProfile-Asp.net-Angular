import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { GameComponent } from './pages/game-pages/game/game.component';
import { GamesComponent } from './pages/game-pages/games/games.component';
import { GameAddComponent } from './pages/game-pages/add-game/add-game.component';
import { LoginComponent } from './pages/profile-pages/login/login.component';
import { AfterLoginSteamComponent } from './pages/profile-pages/after-login-steam/after-login-steam.component';
import { ProfileComponent } from './pages/profile-pages/profile/profile.component';
import { AdminGameComponent } from './pages/admin-pages/admin-game/admin-game.component';

const routes: Routes = [
  {path:'game',component: GameComponent},
  {path:'games',component: GamesComponent},
  {path:'add-game',component: GameAddComponent},
  {path:'login', component:LoginComponent},
  {path:'after-login-steam',component:AfterLoginSteamComponent},
  {path:'profile',component: ProfileComponent},
  {path:'admin/game',component:AdminGameComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
