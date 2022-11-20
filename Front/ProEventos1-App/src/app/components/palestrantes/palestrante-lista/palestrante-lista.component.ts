import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { PaginatedResult, Pagination } from 'src/app/models/Pagination';
import { Palestrante } from 'src/app/models/Palestrante';
import { PalestranteService } from 'src/app/services/palestrante.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-palestrante-lista',
  templateUrl: './palestrante-lista.component.html',
  styleUrls: ['./palestrante-lista.component.scss']
})
export class PalestranteListaComponent implements OnInit {

  constructor(
    private palestranteService: PalestranteService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
    ) { }
  public Palestrantes: Palestrante[] = [];
  public eventoId = 0;
  public pagination = {} as Pagination;

    termoBuscaChanged: Subject<string> = new Subject<string>();

    public ngOnInit(): void {
      this.pagination = {
        currentPage: 1,
        itemsPerPage: 3,
        totalItems: 1
      } as Pagination;

      this.carregarPalestrantes();
    }

  public filtrarPalestrantes(evt: any): void {
    if (this.termoBuscaChanged.observers.length === 0) {
      this.termoBuscaChanged
      .pipe(debounceTime(1000))
      .subscribe(
        filtrarPor => {
          this.spinner.show();
          this.palestranteService
              .getPalestrantes(this.pagination.currentPage,
                          this.pagination.itemsPerPage,
                          filtrarPor
                          ).subscribe(
                            (paginatedResult: PaginatedResult<Palestrante[]>) => {
                              this.Palestrantes = paginatedResult.result;
                              this.pagination = paginatedResult.pagination;
                            },
                            (error: any) => {
                              this.spinner.hide();
                              this.toastr.error('Erro ao carregar os Palestrantes!', '[ERRO!]');
                            }
                          ).add(() => this.spinner.hide());
        });
    }
    this.termoBuscaChanged.next(evt.value);
  }

  public getImagemURL(imagemName: string): string {
    if (imagemName) {
      return environment.apiURL + `resources/perfil/${imagemName}`;
    }
    else
    {
      return './assets/img/upload.png';
    }
  }

  public carregarPalestrantes(): void {
    this.spinner.show();
    this.palestranteService.getPalestrantes(this.pagination.currentPage,
                                  this.pagination.itemsPerPage).subscribe(
      (paginatedResult: PaginatedResult<Palestrante[]>) => {
        this.Palestrantes = paginatedResult.result;
        this.pagination = paginatedResult.pagination;
      },
      (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar os Palestrantes!', '[ERRO!]');
      }
      )
      .add(() => this.spinner.hide());

  }

}
