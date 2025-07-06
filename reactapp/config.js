import config from '../config.json';

const DOMAIN = config.API.Domain;

export const API_CONFIG = {
    domain: DOMAIN,
    XX: `${DOMAIN}${config.API.Paths.X}`,
	...
};