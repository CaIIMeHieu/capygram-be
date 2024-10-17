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
