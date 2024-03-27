export class AppConfig {
    public static get WebAPIBaseUrl(): string {
        const url = process.env.NEXT_PUBLIC_WEB_API_BASE_URL;

        if (undefined == url || "" == url.trim()) {
            throw new Error(`Failed to read WEB_API_BASE_URL environment variable.`);
        }

        return url;
    }
}
