import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  images: {
    remotePatterns: [
      {
        protocol: 'https',
        hostname: 'cdn2.fptshop.com.vn',
        port: '',
        pathname: '/**',
      },
    ],
  },
};

export default nextConfig;
