export interface PrestadorDTO {
    id: number;
    nombres: string;
    primerAp: string;
    segundoAp: string;
    horaEntradaBase: Date;
    horaSalidaBase:Date;
    email:string;
  //apellidos:string; //Nuevo Campo
  // Agrega una firma de índice    
  [key: string]: any;
  }

