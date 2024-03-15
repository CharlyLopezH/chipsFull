//Esto se usa como apoyo para  determinar que ambiente usar (prod o desarrollo)
export const getEnvVariables=()=>{    
    return {                  
        VITE_API_URL: import.meta.env.VITE_API_URL,
    }
}
export default getEnvVariables;