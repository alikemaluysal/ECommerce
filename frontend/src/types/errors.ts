// API Error Types

export interface ValidationError {
  Property: string;
  Errors: string[];
}

export interface ValidationProblemDetails {
  type: string;
  title: string;
  status: number;
  detail: string;
  Errors: ValidationError[];
}

export interface BusinessProblemDetails {
  type: string;
  title: string;
  status: number;
  detail: string;
}

export interface AuthorizationProblemDetails {
  type: string;
  title: string;
  status: number;
  detail: string;
}

export interface NotFoundProblemDetails {
  type: string;
  title: string;
  status: number;
  detail: string;
}

export interface InternalServerErrorProblemDetails {
  type: string;
  title: string;
  status: number;
  detail: string;
}

export type ProblemDetails =
  | ValidationProblemDetails
  | BusinessProblemDetails
  | AuthorizationProblemDetails
  | NotFoundProblemDetails
  | InternalServerErrorProblemDetails;

export const ProblemType = {
  Validation: 'https://example.com/probs/validation',
  Business: 'https://example.com/probs/business',
  Authorization: 'https://example.com/probs/authorization',
  NotFound: 'https://example.com/probs/notfound',
  Internal: 'https://example.com/probs/internal',
} as const;

export class ApiError extends Error {
  public readonly problemDetails: ProblemDetails;
  public readonly status: number;

  constructor(
    problemDetails: ProblemDetails,
    status: number
  ) {
    super(problemDetails.detail);
    this.name = 'ApiError';
    this.problemDetails = problemDetails;
    this.status = status;
  }

  isValidationError(): this is { problemDetails: ValidationProblemDetails } {
    return this.problemDetails.type === ProblemType.Validation;
  }

  isBusinessError(): this is { problemDetails: BusinessProblemDetails } {
    return this.problemDetails.type === ProblemType.Business;
  }

  isAuthorizationError(): this is { problemDetails: AuthorizationProblemDetails } {
    return this.problemDetails.type === ProblemType.Authorization;
  }

  isNotFoundError(): this is { problemDetails: NotFoundProblemDetails } {
    return this.problemDetails.type === ProblemType.NotFound;
  }

  isInternalServerError(): this is { problemDetails: InternalServerErrorProblemDetails } {
    return this.problemDetails.type === ProblemType.Internal;
  }
}
