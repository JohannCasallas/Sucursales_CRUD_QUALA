import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IMonedaJc } from '../interface/IMonedaJc';
import { IRespuesta } from '../interface/IRespuesta';


@Injectable({
  providedIn: 'root'
})
export class MonedaServicio {

  private apiURL: string = '';

  constructor(private http: HttpClient) { }

  obtenerMonedas(): Observable<IRespuesta<IMonedaJc[]>> {
    return this.http.get<IRespuesta<IMonedaJc[]>>(`${this.apiURL}/api/MonedaJcs/ObtenerMonedas`);
  }
}
