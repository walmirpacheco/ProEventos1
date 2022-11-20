import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-palestrantes',
  templateUrl: './palestrantes.component.html',
  styleUrls: ['./palestrantes.component.scss']
})
export class PalestrantesComponent implements OnInit {
  modalRef: BsModalRef;
  public eventoId = 0;
  public formRS: FormGroup;
  public redeSocialAtual = { id: 0, name: '', indice: 0 };

  constructor() { }

  ngOnInit() {
  }

}
