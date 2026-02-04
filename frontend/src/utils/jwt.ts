interface JWTPayload {
  [key: string]: any;
}

export function decodeJWT(token: string): JWTPayload | null {
  try {
    const base64Url = token.split('.')[1];
    if (!base64Url) return null;
    
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );
    
    return JSON.parse(jsonPayload);
  } catch (error) {
    console.error('JWT decode error:', error);
    return null;
  }
}

export function getRolesFromToken(token: string): string[] {
  const payload = decodeJWT(token);
  if (!payload) return [];
  
  const roleClaimKey = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
  const roleClaim = payload[roleClaimKey];
  
  if (!roleClaim) return [];
  
  if (Array.isArray(roleClaim)) {
    return roleClaim;
  }
  
  return [roleClaim];
}

export function hasRole(token: string | null, role: string): boolean {
  if (!token) return false;
  
  const roles = getRolesFromToken(token);
  return roles.includes(role);
}

export function getUserIdFromToken(token: string): string | null {
  const payload = decodeJWT(token);
  if (!payload) return null;
  
  const userIdClaimKey = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier';
  return payload[userIdClaimKey] || null;
}

export function getEmailFromToken(token: string): string | null {
  const payload = decodeJWT(token);
  if (!payload) return null;
  
  const emailClaimKey = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress';
  return payload[emailClaimKey] || null;
}
