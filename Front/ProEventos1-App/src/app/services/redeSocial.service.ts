import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { RedeSocial } from '../models/RedeSocial';

@Injectable({
  providedIn: 'root'
})
export class RedeSocialService {
  baseURL = environment.apiURL + 'api/redesSociais';

constructor(private http: HttpClient) { }

/**
 *
 * @param origem Precisa passar a palavra 'palestrante' ou a palavra 'evento' escrito em minúsculo.
 * @param id Precisa passar o Palestrante ou o EventoId dependendo da sua origem.
 * @returns Observable<RedeSocial[]>
 */
public getRedesSociais(origem: string, id: number): Observable<RedeSocial[]>{
  // tslint:disable-next-line: prefer-const
  let URL =
  id === 0
  ? `${this.baseURL}/${origem}`
  : `${this.baseURL}/${origem}/${id}`;

  return this.http.get<RedeSocial[]>(URL).pipe(take(1));
}

/**
 *
 * @param origem Precisa passar a palavra 'palestrante' ou a palavra 'evento' escrito em minúsculo.
 * @param id Precisa passar o Palestrante ou o EventoId dependendo da sua origem.
 * @param RedesSociais Precisa adicionar Redes Sociais organizadas em RedeSocial[].
 * @returns Observable<RedeSocial[]>
 */
 public saveRedesSociais(
  origem: string,
  id: number,
  redesSociais: [RedeSocial]
  ): Observable<RedeSocial[]>{
  // tslint:disable-next-line: prefer-const
  let URL =
  id === 0
  ? `${this.baseURL}/${origem}`
  : `${this.baseURL}/${origem}/${id}`;

  return this.http.put<RedeSocial[]>(URL, redesSociais).pipe(take(1));
}

/**
 *
 * @param origem Precisa passar a palavra 'palestrante' ou a palavra 'evento' escrito em minúsculo.
 * @param id Precisa passar o Palestrante ou o EventoId dependendo da sua origem.
 * @param redeSocialId Precisa usar o id da Rede Social
 * @returns Observable<any> -- Pois é o retorno da Rota.
 */
 public deleteRedeSocial(
  origem: string,
  id: number,
  redeSocialId: number
  ): Observable<any>{
  // tslint:disable-next-line: prefer-const
  let URL =
  id === 0
  ? `${this.baseURL}/${origem}/${redeSocialId}`
  : `${this.baseURL}/${origem}/${id}/${redeSocialId}`;

  return this.http.delete(URL).pipe(take(1));
}
}
