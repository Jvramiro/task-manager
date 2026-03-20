import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorHandlerInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessage = 'Ocorreu um erro inesperado!';

      if (error.error instanceof ErrorEvent) {
        errorMessage = `Erro: ${error.error.message}`;
      }
      else {
        if (error.error && error.error.detail) {
          errorMessage = error.error.detail;
        } else if (error.error && error.error.title) {
          errorMessage = error.error.title;
        } else if (typeof error.error === 'string') {
          errorMessage = error.error;
        } else {
          errorMessage = `Código do Erro: ${error.status}\nMensagem: ${error.message}`;
        }
      }

      console.error('Erro interceptado:', errorMessage);
      return throwError(() => new Error(errorMessage));
    })
  );
};
