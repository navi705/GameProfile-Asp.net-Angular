import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GameComponent } from './pages/game-pages/game/game.component';
import { GamesComponent } from './pages/game-pages/games/games.component';
import { GameAddComponent } from './pages/game-pages/add-game/add-game.component';
import { LoginComponent } from './pages/shared/login/login.component';

const routes: Routes = [
  {path:'game',component: GameComponent},
  {path:'games',component: GamesComponent},
  {path:'add-game',component: GameAddComponent},
  {path:'login', component:LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
