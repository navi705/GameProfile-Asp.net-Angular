import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { GameComponent } from './pages/game-pages/game/game.component';
import { GamesComponent } from './pages/game-pages/games/games.component';
import { GameAddComponent } from './pages/game-pages/add-game/add-game.component';
import { LoginComponent } from './pages/profile-pages/login/login.component';
import { AfterLoginSteamComponent } from './pages/profile-pages/after-login-steam/after-login-steam.component';
import { ProfileComponent } from './pages/profile-pages/profile/profile.component';
import { AdminGameComponent } from './pages/admin-pages/admin-game/admin-game.component';
import { GameFiltersCreatorsComponent } from './pages/game-pages/game-filters-creators/game-filters-creators.component';
import { StatsPageComponent } from './pages/stats/stats-page/stats-page.component';
import { NotFoundComponent } from './pages/shared/NotFound/not-found/not-found.component';
import { HomePageComponent } from './pages/shared/home/home-page/home-page.component';
import { ProfileViewComponent } from './pages/profile-pages/profile/profile-components/profile-view/profile-view.component';
import { FindTeammeteSeacrhComponent } from './pages/find-temmate-pages/find-teammete-seacrh/find-teammete-seacrh.component';
import { ForumComponent } from './pages/forum-pages/forum/forum.component';
import { AddForumPostComponent } from './pages/forum-pages/add-forum-post/add-forum-post.component';
import { PostComponent } from './pages/forum-pages/post/post.component';
import { AddSteamAccountComponent } from './pages/profile-pages/add-steam-account/add-steam-account/add-steam-account.component';

const routes: Routes = [
  {path:'game',component: GameComponent},
  {path:'games',component: GamesComponent},
  // {path:'game/add-game',component: GameAddComponent},
  {path:'login', component:LoginComponent},
  {path:'after-login-steam',component:AfterLoginSteamComponent},
  {path:'profile',component: ProfileComponent},
  {path:'admin/delete',component:AdminGameComponent},
  {path: 'game/filter',component:GameFiltersCreatorsComponent},
  {path: 'stats',component:StatsPageComponent},
  {path:'',component: HomePageComponent},
  {path:'profile/:id',component: ProfileViewComponent},
  {path:'find-teammate', component: FindTeammeteSeacrhComponent},
  {path:'forum',component: ForumComponent},
  {path:'forum/add',component: AddForumPostComponent},
  {path:'forum/:id',component:PostComponent},
  {path:'add-steam-account',component:AddSteamAccountComponent},
  {path: '**', component: NotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
