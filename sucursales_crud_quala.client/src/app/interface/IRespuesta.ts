export interface IRespuesta<T> {
  exito: boolean;
  mensaje?: string;
  dato?: T;
}
