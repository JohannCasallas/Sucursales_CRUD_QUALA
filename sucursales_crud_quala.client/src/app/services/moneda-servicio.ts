import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IMonedaJc } from '../interface/IMonedaJc';
import { IRespuesta } from '../interface/IRespuesta';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class MonedaServicio {

  private endPoint: string = environment.endPoint;
  private apiURL: string = this.endPoint + "MonedaJcs/";

  constructor(private http: HttpClient) { }

  obtenerMonedas(): Observable<IRespuesta<IMonedaJc[]>> {
    return this.http.get<IRespuesta<IMonedaJc[]>>(`${this.apiURL}ObtenerMonedas`);
  }
}
