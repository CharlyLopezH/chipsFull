//Define las rutas de los diferentes endpoints proporcionados por el servicio API en producción o desarrollo
import {getEnvVariables} from "../helpers/getEnvVariables";
const {VITE_API_URL}=getEnvVariables();
console.log('Dentro de endopoints VITE_API_URL= '+VITE_API_URL);
export const prestadoresEndpoint = `${VITE_API_URL}/Prestadores`