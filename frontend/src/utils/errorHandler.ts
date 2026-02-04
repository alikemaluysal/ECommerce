import { toast } from 'sonner';
import { ApiError } from '../types/errors';

export function handleApiError(error: unknown, customMessage?: string): void {
  if (customMessage) {
    toast.error(customMessage);
    return;
  }

  if (error instanceof ApiError) {
    if (error.isValidationError()) {
      const validationProblem = error.problemDetails as import('../types/errors').ValidationProblemDetails;
      const messages = validationProblem.Errors.flatMap((e) => e.Errors);
      messages.forEach((msg) => {
        toast.error(msg);
      });
      return;
    }
    
    toast.error(error.problemDetails.detail);
  } else if (error instanceof Error) {
    toast.error(error.message);
  } else {
    toast.error('Beklenmeyen bir hata oluştu.');
  }
}

export function getValidationErrors(error: unknown): Record<string, string[]> {
  const errors: Record<string, string[]> = {};

  if (error instanceof ApiError && error.isValidationError()) {
    error.problemDetails.Errors.forEach((validationError) => {
      if (validationError.Property) {
        errors[validationError.Property] = validationError.Errors;
      }
    });
  }

  return errors;
}

export function showSuccess(message: string): void {
  toast.success(message);
}

export function showError(message: string): void {
  toast.error(message);
}

export function showInfo(message: string): void {
  toast.info(message);
}

export function showWarning(message: string): void {
  toast.warning(message);
}
