import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { ContainerComponent } from './container/container.component';
import { ComponentoneComponent } from './componentone/componentone.component';
import { SeedsupportComponent } from './seedsupport/seedsupport.component';
import { FeestructureComponent } from './feestructure/feestructure.component';
import { FormsModule } from '@angular/forms';
import { DialougeboxComponent } from './dialougebox/dialougebox.component';
import { ModalContentComponent } from './modal-content/modal-content.component';
import { GraphModalComponent } from './graph-modal/graph-modal.component';
import { MatDialogModule } from '@angular/material/dialog';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DealermodalComponent } from './dealermodal/dealermodal.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    ContainerComponent,
    ComponentoneComponent,
    SeedsupportComponent,
    FeestructureComponent,
    DialougeboxComponent,
    ModalContentComponent,
    GraphModalComponent,
    DealermodalComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MatDialogModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
