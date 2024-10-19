pipeline {
    agent any

    stages {
        stage('Checkout Code') {
            steps {
                echo 'Checking out code from GitHub...'
                // Lấy mã nguồn từ GitHub
                git branch: 'master', url: 'https://github.com/CaIIMeHieu/capygram-be.git'
            }
        }
        stage('Analysising code with SonaQube') {
            steps {
                script {
                    def sqScannerMsBuildHome = tool 'SonarScanner for .NET'
                    
                    withSonarQubeEnv('SonarCloud') {
                        // Begin SonarQube analysis, providing the SonarCloud project key
                        bat "${sqScannerMsBuildHome}\\SonarScanner.MSBuild.exe begin /k:CaIIMeHieu_capygram-be"
                        
                        // Build the ASP.NET Core project
                        bat 'dotnet build'
                        
                        // End SonarQube analysis
                        bat "${sqScannerMsBuildHome}\\SonarScanner.MSBuild.exe end"
                    }
                }
        }
        }
        stage('Run Docker Script') {
            steps {
                echo 'Running the Docker script...'
                // Cấp quyền thực thi cho script (nếu cần)
                sh 'chmod +x ./run_capygram_docker.sh'
                // Thực thi script quản lý Docker containers
                sh './run_capygram_docker.sh'
            }
        }
    }

    post {
        always {
            echo 'Cleaning up workspace...'
            cleanWs()
        }
    }
}
