import { NgModule } from '@angular/core';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import {MatSelectModule} from '@angular/material/select';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatIconModule} from '@angular/material/icon';
import {ScrollingModule} from '@angular/cdk/scrolling';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatChipsModule} from '@angular/material/chips';
import {MatSliderModule} from '@angular/material/slider';
import {MatBadgeModule} from '@angular/material/badge';
import {MatMenuModule} from '@angular/material/menu';
import {MatDialogModule} from '@angular/material/dialog';
import {MatTooltipModule} from '@angular/material/tooltip';

const MaterialComponents = [MatToolbarModule, MatCardModule,MatButtonModule,MatInputModule,MatCheckboxModule,MatButtonToggleModule,MatSelectModule,MatDatepickerModule
,MatNativeDateModule,MatAutocompleteModule,MatIconModule,ScrollingModule,MatProgressSpinnerModule,MatSnackBarModule,MatChipsModule
,MatSliderModule,MatBadgeModule,MatMenuModule,MatDialogModule,MatTooltipModule];

@NgModule({
imports: [MaterialComponents],
exports: [MaterialComponents]
})
export class MaterialModule{}