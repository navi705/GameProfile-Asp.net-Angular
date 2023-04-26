import { NgModule } from '@angular/core';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatCheckboxModule} from '@angular/material/checkbox';

const MaterialComponents = [MatToolbarModule, MatCardModule,MatButtonModule,MatInputModule,MatCheckboxModule];

@NgModule({
imports: [MaterialComponents],
exports: [MaterialComponents]
})
export class MaterialModule{}